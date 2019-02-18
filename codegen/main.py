import argparse
import logging
import os
import re
import sys

from itsjustcode import generateit, label_case, pascal_case, plural
from sqlalchemy import create_engine

import database


class CodeGenerator(object):

    def __init__(self):
        self.load_logger()
        self.load_args()
        self.load_paths()
        self.load_templates()

    def run_templates(self):
        engine = self.get_database_engine()
        bindings = dict(namespace='Vyger')
        for file in self.templates:
            out_file = self.get_out_file(file)
            template_name = self.get_template_name(file)
            self.log('      Template', template_name)
            self.log('           Out', out_file)
            generateit(engine=engine,
                       table=self.table,
                       template_search=self.template_path,
                       template_file=template_name,
                       out_file=out_file,
                       bindings=bindings)

    def get_database_engine(self):
        return database.create_database_engine()

    def get_out_file(self, file):
        out_path = self.get_out_path(file)
        filename = self.get_filename(file)
        fullname = os.path.join(out_path, filename)
        fullpath = os.path.dirname(fullname)
        if not os.path.exists(fullpath):
            os.makedirs(fullpath)
        return fullname

    def get_out_path(self, file):
        out_path = os.path.dirname(file)
        out_path = out_path.replace(self.template_path, self.solution_path)
        if 'core-components' in out_path:
            #   create a path
            #   pages/table/name
            names = label_case(self.table).lower().split(' ')
            tree_path = '-'.join(names)
            out_path = os.path.join(out_path, tree_path)
        return out_path

    def get_filename(self, file):
        filename = os.path.basename(file).replace('.j2', '')
        if 'core-components' in file:
            #   create a file name
            #   table-name/table-name-component/table-name-component.ext
            pos = filename.find('.')
            names = label_case(self.table).lower().split(' ')
            component = '-'.join(names) + '-' + filename[:pos]
            filename = '-'.join(names) + '-' + filename
            filename = os.path.join(component, filename)
        else:
            #   special case for when NOT in Pages
            #   include table name
            filename = pascal_case(self.table) + filename
        return filename

    def get_template_name(self, file):
        template_name = file.replace(self.template_path, '')
        return template_name[1:].replace('\\', '/')

    def load_logger(self):
        root = logging.getLogger()
        formatter = logging.Formatter('%(asctime)s %(message)s', '%H:%M:%S')
        root.setLevel('DEBUG')

        ch = logging.StreamHandler(sys.stdout)
        ch.setLevel('DEBUG')
        ch.setFormatter(formatter)
        root.addHandler(ch)

        self.logger = logging.getLogger(__name__)

    def load_paths(self):
        my_path = os.path.abspath(os.path.dirname(__file__))
        go_up = '../vyger-app/src'
        self.solution_path = os.path.abspath(os.path.join(my_path, go_up))
        self.template_path = os.path.join(my_path, 'templates')
        self.log('Solution Path', self.solution_path)
        self.log('Template Path', self.template_path)

    def load_args(self):
        parser = argparse.ArgumentParser()
        parser.add_argument('-t', action='store', dest='table')
        parser.add_argument('-x', action='store', dest='template')
        args = parser.parse_args()
        self.table = args.table
        self.template = args.template
        self.log('ARG:Table', self.table)
        self.log('ARG:Template', self.template)

    def load_templates(self):
        self.templates = []
        pattern = self.template.replace('*', '.*')
        regex = re.compile(f'^{pattern}.j2$', re.IGNORECASE)
        for dirname, _, files in os.walk(self.template_path):
            for filename in files:
                if regex.match(filename):
                    template_file = os.path.join(dirname, filename)
                    self.templates.append(template_file)
        self.log('Total Templates', len(self.templates))

    def log(self, title, message):
        self.logger.info(f'{title.ljust(16)}: {message}')


if __name__ == '__main__':
    gen = CodeGenerator()
    gen.run_templates()
