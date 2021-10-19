import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {  ReactiveFormsModule } from '@angular/forms';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';




@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatSlideToggleModule,
    
    
  ] ,
  exports: [
    CommonModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatSlideToggleModule,
  ],
})
export class SharedModule { }
