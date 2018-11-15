let id = 0;

export enum FlashMessageSeverity
{
    Information = 'Information',
    Warning = 'Warning',
    Danger = 'Danger'
}

export interface IFlashMessage
{
    severity?: FlashMessageSeverity;
    message?: string;
    label?: string;
}

export class FlashMessage
{
    id: number;
    severity: FlashMessageSeverity;
    message: string;
    label: string;

    constructor(properties?: IFlashMessage)
    {
        this.id = ++id;
        this.severity = FlashMessageSeverity.Information;

        if (properties)
        {
            this.severity = properties.severity || this.severity;
            this.message = properties.message || this.message;
            this.label = properties.label || this.label;
        }
    }
}
