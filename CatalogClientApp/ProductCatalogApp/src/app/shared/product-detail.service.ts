import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { ProductDetail } from './product-detail.model';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ProductDetailService {

  url: string = environment.apiBaseUrl + 'products'
  list:ProductDetail[] = []
  formData : ProductDetail = new ProductDetail()
  formSubmitted: boolean = false;
  constructor(private http: HttpClient) { }

  refreshList(){
    this.http.get(this.url)
    .subscribe({
      next: res => {
           this.list= res as ProductDetail[]

      },
      error: err => { console.log(err) 
      }
    })

  }
  postProductDetail(){

   return this.http.post(this.url, this.formData)
  }
  
  putProductDetail(){

    return this.http.post(this.url+'/'+this.formData.id, this.formData)
   }

   deleteProductDetail(id:string){

    return this.http.delete(this.url+'/'+this.formData.id)
   }

  resetForm(form:NgForm){
    form.form.reset()
    this.formData= new ProductDetail()
    this.formSubmitted= false
  }
}
