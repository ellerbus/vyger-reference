import { Component, OnInit } from '@angular/core';

import { DataService } from '../../services/data.service';
import { FileInfo } from '../../models/file-info';

@Component({
    selector: 'app-data',
    templateUrl: './data.page.html',
    styleUrls: ['./data.page.css']
})
export class DataPageComponent implements OnInit {
    files: FileInfo[];

    constructor(
        public dataService: DataService
    ) { }

    ngOnInit() {
        console.log('getting files');
        this.dataService.getFiles().then((data: FileInfo[]) => this.files = data);
    }
}
