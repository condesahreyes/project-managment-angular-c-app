import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bug } from 'src/app/models/bug/Bug';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImportService {

  constructor(
    private http: HttpClient) { }

  private uri: string = `${environment.URI_BASE}/import`;

  import(path: string) : Observable<Bug[]>{
    return this.http.post<Bug[]>(this.uri + '/bugs', {      
      FileAddress: path
    });
  }

}
