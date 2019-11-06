import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SportEventsService {
    baseUrl: string;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    GetAllForPreview() {
        let reqHeader = new HttpHeaders();
        reqHeader.append('Content-Type', 'application/json');
        return this.http.get(this.baseUrl + 'api/SportEvents/GetSportEvents', { headers: reqHeader });
    }

    CreateDefaultSportEvent() {
        let reqHeader = new HttpHeaders();
        reqHeader.append('Content-Type', 'application/json');
        return this.http.post(this.baseUrl + 'api/SportEvents/CreateDeafultSportEvent', { headers: reqHeader });
    }

    DeleteSportEvent(id: number) {
        let reqHeader = new HttpHeaders();
        reqHeader.append('Content-Type', 'application/json');
        return this.http.delete(this.baseUrl + 'api/SportEvents/DeleteSportEvent/' + id, { headers: reqHeader });
    }

    UpdateSportEvent(id: number, formData) {
        return this.http.put(this.baseUrl + 'api/SportEvents/UpdateSportEvent/' + id, formData);
    }
}

