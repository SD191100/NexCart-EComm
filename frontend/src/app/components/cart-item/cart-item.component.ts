import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from '../../models/product/product';
import { CartItem } from '../../models/cart/cartItem.model';
import { CartService } from '../../services/cart.service';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-cart-item',
  imports: [],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent implements OnInit {
  @Input() item!: CartItem;
  @Output() itemUpdated = new EventEmitter<void>();
   productDetail: Product | null = null;



  constructor(private cartService: CartService, private productService: ProductService) {}

  ngOnInit () {
    console.log(this.item)
    this.productService.getProductById(this.item.productId).subscribe(
      (product) => {this.productDetail = product;
        console.log(product)
      }
    )
  }

  increaseQuantity(): void {
    this.cartService.updateCartItem(this.item.cartItemId, this.item.quantity + 1).subscribe(() => {
      this.itemUpdated.emit();
    });
  }

  decreaseQuantity(): void {
    if (this.item.quantity > 1) {
      this.cartService.updateCartItem(this.item.cartItemId, this.item.quantity - 1).subscribe(() => {
        this.itemUpdated.emit();
      });
    }
  }

  removeItem(): void {
    this.cartService.removeCartItem(this.item.cartItemId).subscribe(() => {
      this.itemUpdated.emit();
    });
  }
}
