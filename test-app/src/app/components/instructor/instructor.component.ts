import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Instructor } from 'src/app/models/Instructor';

@Component({
    selector: 'instructor',
    templateUrl: './instructor.component.html',
})
export class InstructorComponent {
    constructor(
      public dialogRef: MatDialogRef<InstructorComponent>,
      @Inject(MAT_DIALOG_DATA) public data: Instructor) {}
    
      onCancelClick(): void {
        this.dialogRef.close();
      }
}