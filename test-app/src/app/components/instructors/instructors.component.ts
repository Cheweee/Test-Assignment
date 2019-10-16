import { OnInit, Component } from '@angular/core';
import { Instructor } from 'src/app/models/Instructor';
import { InstructorsService } from 'src/app/services/instructors.service';
import { MatDialog } from '@angular/material/dialog';
import { InstructorComponent } from '../instructor/instructor.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    selector: 'instructors',
    templateUrl: './instructors.component.html',
    styleUrls: ['./instructors.component.css', '../../app.component.css']
})
export class InstructorsComponent implements OnInit {
    instructors: MatTableDataSource<Instructor>;
    id?: number;
    loading: boolean;
    displayedColumns: string[] = ['id', 'edit', 'firstName', 'secondName', 'lastName', 'delete'];

    constructor(public dialog: MatDialog, private _service: InstructorsService) {
        this.instructors = new MatTableDataSource<Instructor>();
    }

    ngOnInit(): void {
        this.getInstructors();
    }

    add(): void {
        const dialogRef = this.dialog.open(InstructorComponent, {
            data: new Instructor()
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result)
                this.createInstructor(result);
        });
    }

    edit(instructor: Instructor): void {
        const dialogRef = this.dialog.open(InstructorComponent, {
            data: { ...instructor }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.updateInstructor(result);
            }
            else {
                this.getInstructors();
            }
        });
    }

    delete(id: number): void {
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
            data: { message: 'Are you sure want to delete instructor?' }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.deleteInstructor(id);
            }
        })
    }

    filter(filterValue: string): void {
        this.instructors.filter = filterValue;
    }

    private getInstructors(): void {
        this.loading = true;
        this._service.get({ id: this.id })
            .subscribe(result => {
                this.instructors.data = result;
                this.loading = false;
            });
    }

    private createInstructor(instructor: Instructor): void {
        this.loading = true;
        this._service.create(instructor)
            .subscribe(() => this.getInstructors());
    }

    private updateInstructor(instructor: Instructor): void {
        this.loading = true;
        this._service.update(instructor)
            .subscribe(() => this.getInstructors());
    }

    private deleteInstructor(id: number): void {
        this.loading = true;
        this._service.delete(id).subscribe(() => this.getInstructors());
    }
}