import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ImportService } from 'src/app/services/import/import.service';

@Component({
  selector: 'app-bug-import-form',
  templateUrl: './bug-import-form.component.html',
  styleUrls: ['./bug-import-form.component.css']
})
export class BugImportFormComponent implements OnInit {

  //path : string ="";


  constructor(
    private importService: ImportService,
    public dialogRef: MatDialogRef<BugImportFormComponent>
    ) { }
    
  path = new FormControl('', Validators.required);

  public form = new FormGroup({
    pathValue: this.path
  });

  ngOnInit(): void {
  }

  import(){
    const myPath = this.form.value.pathValue;  
    console.log(myPath + " esoo");
    this.importService.import(myPath).subscribe(() => this.close());
  }

  close() {
    this.dialogRef.close();
  }

}
