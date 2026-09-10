import {
  Component,
  inject
} from '@angular/core';

import {
  Router
} from '@angular/router';

import {
  ProductFormComponent
} from '../../components/product-form/product-form';

import {
  ProductService
} from '../../services/product.service';

import {
  Product
} from '../../models/product';

import {
  CreateProduct
} from '../../models/create-product';

@Component({

  selector: 'app-product-create',

  standalone: true,

  imports: [
    ProductFormComponent
  ],

  templateUrl: './product-create.html',

  styleUrl: './product-create.scss'

})
export class ProductCreateComponent {

  private productService =
    inject(ProductService);

  private router =
    inject(Router);

  create(product: Product): void {

    const dto: CreateProduct = {

      name: product.name,

      description: product.description,

      price: product.price,

      stock: product.stock,

      isActive: product.isActive

    };

    this.productService
      .create(dto)
      .subscribe(createdProduct => {

        console.log(
          'Producto creado:',
          createdProduct
        );

        this.router.navigate([
          '/products/detail',
          createdProduct.id
        ]);

      });

  }

}