import { Component } from '@angular/core';

import { RouterOutlet } from '@angular/router';

import { PageHeader }
from '../../shared/components/page-header/page-header';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    PageHeader
  ],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss'
})

export class MainLayoutComponent {}