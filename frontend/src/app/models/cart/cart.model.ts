import { CartItem } from './cartItem.model';

export interface Cart {
  cartId: number;
  userId: number;
  cartItems: CartItem[];
}

