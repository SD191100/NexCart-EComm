import { Component } from '@angular/core';
import { CarouselComponent } from '../../components/carousel/carousel.component';
import { FeaturedComponent } from '../../components/featured/featured.component';
import { ProductBrowsingComponent } from '../product-details/product-details.component';

@Component({
  selector: 'app-home',
  imports: [CarouselComponent, FeaturedComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
