import { Injectable } from '@angular/core';
import { User } from '../models/user';

const CLIENT_ID = '287029206626-rmffeodje647m9fnb78lanj8arnks303.apps.googleusercontent.com';
const API_KEY = 'AIzaSyBHWWeW40pg6Pe7tyJDhGt1UqyoWpRkiM0';
const DISCOVERY_DOCS = ['https://www.googleapis.com/discovery/v1/apis/drive/v3/rest'];
var SCOPES = 'email https://www.googleapis.com/auth/plus.me https://www.googleapis.com/auth/drive.appdata';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    googleAuth: gapi.auth2.GoogleAuth;
    member: User;

    constructor() {
    }

    initClient() {
        return new Promise((resolve, reject) => {
            gapi.load('client:auth2', () => {
                const options = {
                    apiKey: API_KEY,
                    clientId: CLIENT_ID,
                    discoveryDocs: DISCOVERY_DOCS,
                    scope: SCOPES,
                };

                return gapi.client
                    .init(options).then(() => {
                        this.googleAuth = gapi.auth2.getAuthInstance();
                        if (this.getIsSignedIn()) {
                            this.onsignIn(this.googleAuth.currentUser.get());
                        }
                        resolve();
                    });
            });
        });
    }

    signIn(): Promise<any> {
        const options = {
            prompt: 'consent'
        };

        return new Promise((resolve, reject) => {
            this.googleAuth.signIn(options).then((user: gapi.auth2.GoogleUser) => {
                this.onsignIn(user);
                resolve();
            })
        });
    }

    private onsignIn = (googleUser: gapi.auth2.GoogleUser): void => {
        this.member = User.fromBasicProfile(googleUser.getBasicProfile());
    }

    getIsSignedIn(): boolean {
        let signedin = this.googleAuth && this.googleAuth.isSignedIn.get();

        return signedin;
    }
}