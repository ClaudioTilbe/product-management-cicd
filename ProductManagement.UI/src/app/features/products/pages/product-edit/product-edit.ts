import {
  Component,
  inject,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';


import {
  ActivatedRoute,
  Router
} from '@angular/router';


import {
  CommonModule
} from '@angular/common';


import { ProductFormComponent }
from '../../components/product-form/product-form';


import { ProductService }
from '../../services/product.service';


import { Product }
from '../../models/product';


import { UpdateProduct }
from '../../models/update-product';



@Component({
  selector: 'app-product-edit',

  standalone: true,

  imports: [
    CommonModule,
    ProductFormComponent
  ],

  templateUrl: './product-edit.html',

  styleUrl: './product-edit.scss'
})
export class ProductEditComponent
implements OnInit {


  private route =
    inject(ActivatedRoute);



  private router =
    inject(Router);



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





  loadProduct(id:number):void {


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






  update(product:Product):void {


    const dto: UpdateProduct = {


      name: product.name,


      description: product.description,


      price: product.price,


      stock: product.stock,


      isActive: product.isActive


    };



    this.productService

      .update(
        product.id,
        dto
      )

      .subscribe(() => {



        console.log(
          'Producto actualizado'
        );



        this.router.navigate([
          '/products'
        ]);



      });



  }



}