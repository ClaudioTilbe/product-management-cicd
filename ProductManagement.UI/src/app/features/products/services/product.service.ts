import { Injectable, inject } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import { Product } from '../models/product';

import { CreateProduct } from '../models/create-product';

import { UpdateProduct } from '../models/update-product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private http = inject(HttpClient);

  private apiUrl =
      `${environment.apiUrl}/products`;

  getAll(): Observable<Product[]> {

    return this.http.get<Product[]>(
      this.apiUrl);
  }

  getById(id: number): Observable<Product> {

    return this.http.get<Product>(
      `${this.apiUrl}/${id}`);
  }

  create(dto: CreateProduct): Observable<Product> {

    return this.http.post<Product>(
      this.apiUrl,
      dto);
  }

  update(
      id: number,
      dto: UpdateProduct
  ): Observable<void> {

    return this.http.put<void>(
      `${this.apiUrl}/${id}`,
      dto);
  }

  delete(id: number): Observable<void> {

    return this.http.delete<void>(
      `${this.apiUrl}/${id}`);
  }
}