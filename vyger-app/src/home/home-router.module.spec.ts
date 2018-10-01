import { HomeRouterModule } from './home-router.module';

describe('HomeRouterModule', () => {
    let homeRouterModule: HomeRouterModule;

    beforeEach(() => {
        homeRouterModule = new HomeRouterModule();
    });

    it('should create an instance', () => {
        expect(homeRouterModule).toBeTruthy();
    });
});
