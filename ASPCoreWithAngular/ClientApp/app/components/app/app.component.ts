import { HostListener, Component } from '@angular/core';
import { CookieService } from "angular2-cookie/core";
import { Router } from '@angular/router';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {


    @HostListener('window:beforeunload') goToPage() {
        this.router.navigate(['/dashboard']);
    }

    constructor(private router: Router, private _cookieService: CookieService) {
    }

    ngOnInit() {
        this.router.navigate(['']);

        /*var userLoggedIn = this._cookieService.get("UserName");
        if (userLoggedIn != null)
            console.log(userLoggedIn);*/
       // else
           // window.location.href = "http://localhost:3122/";
    }


}
