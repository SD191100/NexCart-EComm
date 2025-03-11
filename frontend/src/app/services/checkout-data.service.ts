import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CheckoutDataService {
 private checkoutData: any;
  private orderData: any;

  setCheckoutData(data: any): void {
    this.checkoutData = data;
  }

  getCheckoutData(): any {
    return this.checkoutData;
  }

  clearCheckoutData(): void {
    this.checkoutData = null;
  }


  setOrderData(data: any): void {
    this.orderData = data;
  }

  getOrderData(): any {
    return this.orderData;
  }

  clearOrderData(): void {
    this.orderData = null;
  }
  constructor() { }
}
