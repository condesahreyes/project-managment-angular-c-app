import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeveloperService } from 'src/app/services/developer/developer.service';

@Component({
  selector: 'app-bugs-resolved-form',
  templateUrl: './bugs-resolved-form.component.html',
  styleUrls: ['./bugs-resolved-form.component.css']
})
export class BugsResolvedFormComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<BugsResolvedFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private developerService: DeveloperService
  ) { }

  countsBugsResolved: number = 0;
  ngOnInit(): void {
    this.getCountsBugsResolvedByUser();
  }

  getCountsBugsResolvedByUser() {
    this.developerService.getCountBugsResolvedByDeveloper(this.data.id).subscribe(b =>
      this.countsBugsResolved = b);
  }

  close() {
    this.dialogRef.close(true);
  }
}
