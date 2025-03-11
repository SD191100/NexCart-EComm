import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-carousel',
  imports: [FormsModule, CommonModule],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css',
})

export class CarouselComponent implements AfterViewInit {
  @Input() images: string[] = []; // Array of image URLs
  @ViewChild('carousel') carousel!: ElementRef<HTMLDivElement>;
  currentIndex = 0;

  ngAfterViewInit() {
    this.updateCarousel();
  }

  nextSlide(): void {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
    this.updateCarousel();
  }

  prevSlide(): void {
    this.currentIndex = (this.currentIndex - 1 + this.images.length) % this.images.length;
    this.updateCarousel();
  }

  updateCarousel(): void {
    const carouselElement = this.carousel.nativeElement;
    carouselElement.style.transform = `translateX(-${this.currentIndex * 100}%)`;
  }

  goToSlide(index: number): void {
    this.currentIndex = index;
    this.updateCarousel();
  }
}


