// import { async, ComponentFixture, TestBed } from '@angular/core/testing';
// import { RouterTestingModule } from '@angular/router/testing';

// import { NavigationComponent } from './navigation.component';
// import { AuthenticationService } from '../../services/authentication.service';

// describe('NavigationComponent', () => {
//   let component: NavigationComponent;
//   let fixture: ComponentFixture<NavigationComponent>;
//   let mockAuthenticationService: jasmine.SpyObj<AuthenticationService>;

//   beforeEach(async(() => {
//     const authenticationServiceSpy = jasmine.createSpyObj('AuthenticationService', ['getIsSignedIn']);

//     let options = {
//       declarations: [NavigationComponent],
//       imports: [RouterTestingModule],
//       providers: [
//         { provide: AuthenticationService, useValue: authenticationServiceSpy }
//       ]
//     };

//     TestBed.configureTestingModule(options).compileComponents();
//   }));

//   beforeEach(() => {
//     mockAuthenticationService = TestBed.get(AuthenticationService);
//     fixture = TestBed.createComponent(NavigationComponent);
//     component = fixture.componentInstance;
//     fixture.detectChanges();
//   });

//   it('should create', () => {
//     //  arrange
//     //  act
//     //  assert
//     expect(component).toBeTruthy();
//   });

//   describe('getSignedIn', () => {
//     it('should follow AuthenticationService', () => {
//       //  arrange
//       mockAuthenticationService.getIsSignedIn.and.returnValue(true);
//       //  act
//       let actual = component.isSignedIn();
//       //  assert
//       expect(actual).toBeTruthy();
//     });
//   });
// });
