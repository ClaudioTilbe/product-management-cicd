import { Routes } from '@angular/router';

import { MainLayoutComponent }
from './layouts/main-layout/main-layout';

import { ProductListComponent }
from './features/products/pages/product-list/product-list';

import { ProductCreateComponent }
from './features/products/pages/product-create/product-create';

import { ProductEditComponent }
from './features/products/pages/product-edit/product-edit';

import { ProductDetailComponent }
from './features/products/pages/product-detail/product-detail';

export const routes: Routes = [

  {
    path: '',

    component: MainLayoutComponent,

    children: [

      {
        path: '',
        redirectTo: 'products',
        pathMatch: 'full'
      },

      {
        path: 'products',
        component: ProductListComponent
      },

      {
        path: 'products/create',
        component: ProductCreateComponent
      },

      {
        path:'products/detail/:id',
        component: ProductDetailComponent
      },

      {
        path: 'products/edit/:id',
        component: ProductEditComponent
      }

    ]
  }

];