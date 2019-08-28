import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { UserListService } from '../../services/userlistservice.service'

@Component({
    selector: 'getuser',
    templateUrl: './getuser.component.html'
})
export class GetUserComponent {

    public userList = null;

    constructor(public http: Http, private _router: Router, private _userListService: UserListService) {
        this.getUsers();
    }

    getUsers() {
        this._userListService.getUsers().subscribe((m: any) => {
            //console.log(m);
            //console.log(this.pageNo);
            this.userList = m;
            console.log("User Data");
            console.log(this.userList);
        });      
        console.log("Hi");
    }

    logOut() {
        this._userListService.logOut().subscribe((m: any) => {
            console.log("User Logout");
            console.log(m);
            if (m == "true")
                window.location.href = "http://localhost:3122/";
        });        
    }
    
}

interface UserData {
    Id: number;
    FirstName: string;
    LastName: string;
    Email: string;
    Mobile: string;
}