export class User
{

    id: string;
    email: string;
    name: string;
    givenName: string;
    familyName: string;
    imageUrl: string;

    static fromBasicProfile(profile: gapi.auth2.BasicProfile): User
    {
        let user = new User();
        user.id = profile.getId();
        user.email = profile.getEmail();
        user.name = profile.getName();
        user.givenName = profile.getGivenName();
        user.familyName = profile.getFamilyName();
        user.imageUrl = profile.getImageUrl();
        return user;
    }
}
