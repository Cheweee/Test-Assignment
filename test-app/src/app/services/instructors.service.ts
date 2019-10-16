import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Instructor } from '../models/Instructor';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class InstructorsService {
    private apiUrl = "api/instructor";

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    constructor(
        private http: HttpClient
    ) { }

    public get(options: { id?: number, search?: string }): Observable<Instructor[]> {
        let url: string = `${this.apiUrl}`;
        let conditionIndex = 0;
        if(options.id) {
            url += (conditionIndex++ ? '&' : '?') + `id=${options.id}`;
        }
        if(options.search) {
            url += (conditionIndex++ ? '&' : '?') + `search=${options.search}`;
        }
        return this.http.get<Instructor[]>(url);
    }

    public create(instructor: Instructor): Observable<Instructor> {
        return this.http.post<Instructor>(this.apiUrl, instructor, this.httpOptions);
    }

    public update(instructor: Instructor): Observable<Instructor> {
        return this.http.put<Instructor>(this.apiUrl, instructor, this.httpOptions);
    }

    public delete(id: number): Observable<any> {
        let url = `${this.apiUrl}?id=${id}`
        return this.http.delete(url);
    }
}