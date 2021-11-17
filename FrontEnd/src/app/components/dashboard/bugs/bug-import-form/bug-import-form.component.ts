import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ImportService } from 'src/app/services/import/import.service';

@Component({
  selector: 'app-bug-import-form',
  templateUrl: './bug-import-form.component.html',
  styleUrls: ['./bug-import-form.component.css']
})
export class BugImportFormComponent implements OnInit {

  errorMesage: string = "";

  constructor(
    private importService: ImportService,
    public dialogRef: MatDialogRef<BugImportFormComponent>,
    private snackBar: MatSnackBar,
  ) { }

  path = new FormControl('', Validators.required);

  public form = new FormGroup({
    pathValue: this.path
  });

  ngOnInit(): void {
  }

  import() {
    const myPath = this.form.value.pathValue;
    
    this.importService.import(myPath).subscribe(() => {
      this.close();
    }, error => {
      this.errorMesage = error.error;
      this.error(this.errorMesage);
    });
  }

  error(message: string) {
    this.snackBar.open(message, 'error', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom'
    });
  }

  close() {
    this.dialogRef.close();
  }

}
