import { Evento } from './../models/Evento';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from '@environments/environment';

@Injectable(
  // {providedIn: 'root'}
  )

  export class EventoService {
    baseURL = environment.apiURL + 'api/eventos';
    tokenHeader = new HttpHeaders({ Authorization: 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJhZ25lbG8iLCJuYmYiOjE2NTYzMjg4MTMsImV4cCI6MTY1NjQxNTIxMywiaWF0IjoxNjU2MzI4ODEzfQ.zICwEW9xVDPEToY6oAGUJSutPKtCl71jRyJtj9Dr_LJl_o7yOJcHXFnj2DPFY0OcQnFv4zyxq3Rqxsne1kD4bw' });

    constructor(private http: HttpClient) { }

    public getEventos(): Observable<Evento[]> {
      return this.http.get<Evento[]>(this.baseURL, { headers: this.tokenHeader }).pipe(take(1));
    }

    public getEventosByTema(tema: string): Observable<Evento[]> {
      return this.http
      .get<Evento[]>(`${this.baseURL}/${tema}/tema`)
      .pipe(take(1));
    }

    public getEventoById(id: number): Observable<Evento> {
      return this.http
      .get<Evento>(`${this.baseURL}/${id}`)
      .pipe(take(1));
    }

    public post(evento: Evento): Observable<Evento> {
      return this.http
      .post<Evento>(this.baseURL, evento)
      .pipe(take(1));
    }

    public put(evento: Evento): Observable<Evento> {
      return this.http
      .put<Evento>(`${this.baseURL}/${evento.id}`, evento)
      .pipe(take(1));
    }

    public deleteEvento(id: number): Observable<any> {
      return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
    }

    postUpload(eventoId: number, file: File): Observable<Evento> {
      const fileToUpload = file[0] as File;
      const formData = new FormData();
      formData.append('file', fileToUpload);

      return this.http
        .post<Evento>(`${this.baseURL}/upload-image/${eventoId}`, formData)
        .pipe(take(1));
    }
  }
