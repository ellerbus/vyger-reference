let id = 0;

export enum FlashMessageSeverity
{
    Information = 'Information',
    Success = 'Success',
    Danger = 'Danger'
}

export interface IFlashMessage
{
    severity?: FlashMessageSeverity;
    message?: string;
    growl?: boolean;
}

export class FlashMessage
{
    id: number;
    severity: FlashMessageSeverity;
    message: string;
    growl: boolean;
    seconds: number;

    constructor(properties?: IFlashMessage)
    {
        this.id = ++id;
        this.severity = FlashMessageSeverity.Information;
        this.seconds = 0;

        if (properties)
        {
            this.severity = properties.severity || this.severity;
            this.message = properties.message || this.message;
            this.growl = properties.growl || this.growl;
        }
    }
}
