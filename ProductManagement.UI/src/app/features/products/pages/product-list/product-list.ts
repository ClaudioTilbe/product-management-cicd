import {
  Component,
  inject,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { RouterModule } from '@angular/router';

import {
  CommonModule
} from '@angular/common';

import {
  FormsModule
} from '@angular/forms';

import {
  MatButtonModule
} from '@angular/material/button';

import {
  MatIconModule
} from '@angular/material/icon';

import { Product } from '../../models/product';

import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss'
})
export class ProductListComponent
implements OnInit {

  private productService =
      inject(ProductService);

  private cdr =
      inject(ChangeDetectorRef);

  products: Product[] = [];

  filteredProducts: Product[] = [];

  searchId: number | null = null;

  ngOnInit(): void {

    this.loadProducts();
  }

  loadProducts(): void {

    this.productService
      .getAll()
      .subscribe(products => {

        this.products = products;

        this.filteredProducts = products;

        this.cdr.detectChanges();
      });
  }

  searchById(): void {

    if (this.searchId === null) {

      this.filteredProducts = this.products;

      return;
    }

    this.filteredProducts =
      this.products.filter(
        product => product.id === this.searchId
      );
  }

  deleteProduct(id:number):void {


    const confirmDelete =
      confirm(
        'Are you sure you want to delete this product?'
      );


    if(!confirmDelete)
    {
      return;
    }



    this.productService
        .delete(id)
        .subscribe(() => {

          this.loadProducts();


        });


  }


}