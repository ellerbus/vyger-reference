export class BreadCrumb
{
    constructor(
        public title: string,
        public path: string,
        public active: boolean,
        public filter: string)
    {
    }
}
