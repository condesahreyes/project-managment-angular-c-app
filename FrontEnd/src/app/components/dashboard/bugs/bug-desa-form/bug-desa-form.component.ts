import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UsersControllerService } from 'src/app/controllers/users-controller.service';
import { BugState } from 'src/app/models/bug/BugState';
import { BugUpdate } from 'src/app/models/bug/BugUpdate';
import { UserEntryModel } from 'src/app/models/users/UserEntryModel';

@Component({
  selector: 'app-bug-desa-form',
  templateUrl: './bug-desa-form.component.html',
  styleUrls: ['./bug-desa-form.component.css']
})
export class BugDesaFormComponent implements OnInit {

  user!: UserEntryModel;
  errorMesage: string = "";
  newState: string = "";
  bug: any = this.data.bug;

  constructor(
    private snackBar: MatSnackBar,
    private userController: UsersControllerService,
    public dialogRef: MatDialogRef<BugDesaFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }
 
  bugUpdate: BugUpdate = {
    Project: "",
    Name: "",
    Domain: "",
    Version: "",
    State: "",
    UserId: "",
    Duration: 0
  }

  bugState: BugState = {
    State: "",
    Id: 0
  }

  ngOnInit(): void { }

  update() {
    this.bugState.State = this.newState;
    this.bugState.Id = this.bug.id;
    this.userController.updateBug(this.bugState).subscribe(() => {
      this.close();
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
    });
  }

  close() {
    this.dialogRef.close(true)
  }

  error(message: string) {
    this.snackBar.open(message, 'error', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom'
    });
  }
}
