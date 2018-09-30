export class Utilities {
    static generateId(prefix: string, length: number): string {
        const possible = '0123456789abcdefghijklmnopqrstuvwxyz';
        let id = [];

        for (let i = 0; i < length; i++) {
            id.push(possible.charAt(Math.floor(Math.random() * possible.length)));
        }

        return id.join('');
    }

}
