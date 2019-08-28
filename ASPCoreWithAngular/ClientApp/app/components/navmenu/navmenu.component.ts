import { Component } from '@angular/core';
import { CookieService } from "angular2-cookie/core";
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { UserListService } from '../../services/userlistservice.service'


@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    public userLoggedIn = "";

    constructor(private _cookieService: CookieService, public http: Http, private _router: Router, private _userListService: UserListService) {
    }

    ngOnInit() {
        this.userLoggedIn = this._cookieService.get("UserName");
        console.log("Inside Menu");
        console.log(this.userLoggedIn);
    }


    logOut() {
        this._userListService.logOut().subscribe((m: any) => {

            //this._router.navigate(['/dashboard']);
          window.location.href = "http://localhost:3122/";
        });
    }

   

}
