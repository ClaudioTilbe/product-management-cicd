import {
  Component,
  inject,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import {
  ActivatedRoute,
  RouterLink
} from '@angular/router';

import {
  CommonModule
} from '@angular/common';

import {
  MatButtonModule
} from '@angular/material/button';

import {
  MatIconModule
} from '@angular/material/icon';

import {
  ProductService
} from '../../services/product.service';

import {
  Product
} from '../../models/product';


@Component({
  selector: 'app-product-detail',

  standalone: true,

  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
  ],

  templateUrl: './product-detail.html',

  styleUrl: './product-detail.scss'
})
export class ProductDetailComponent implements OnInit {

  private route =
    inject(ActivatedRoute);

  private productService =
    inject(ProductService);

  private cdr =
    inject(ChangeDetectorRef);

  product?: Product;

  ngOnInit(): void {

    const id =
      Number(
        this.route.snapshot.paramMap.get('id')
      );

    this.loadProduct(id);

  }

  loadProduct(id: number): void {

    this.productService
      .getById(id)
      .subscribe(product => {

        this.product = product;

        console.log(
          'Producto cargado:',
          this.product
        );

        this.cdr.detectChanges();

      });

  }

}