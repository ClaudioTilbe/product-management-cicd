import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges
} from '@angular/core';


import { CommonModule }
from '@angular/common';


import { FormsModule }
from '@angular/forms';


import { MatFormFieldModule }
from '@angular/material/form-field';


import { MatInputModule }
from '@angular/material/input';


import { MatCheckboxModule }
from '@angular/material/checkbox';


import { MatButtonModule }
from '@angular/material/button';


import { Product }
from '../../models/product';



@Component({

  selector: 'app-product-form',

  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule
  ],

  templateUrl: './product-form.html',

  styleUrl: './product-form.scss'

})
export class ProductFormComponent
implements OnChanges {



  @Input()
  product?: Product;


  @Output()
  saveProduct =
  new EventEmitter<Product>();



  formProduct: Product = {

    id: 0,

    name: '',

    description: '',

    price: 0,

    stock: 0,

    isActive: true

  };



  ngOnChanges(
    changes: SimpleChanges
  ): void {


    if(
      changes['product']
      &&
      this.product
    ) {


      this.formProduct = {

        ...this.product

      };


    }


  }



  save(): void {

  this.saveProduct.emit(
    this.formProduct
  );


}


}