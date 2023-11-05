import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ProductDetail } from 'src/app/shared/product-detail.model';
import { ProductDetailService } from 'src/app/shared/product-detail.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-product-detail-form',
  templateUrl: './product-detail-form.component.html',
  styles: [
  ]
})
export class ProductDetailFormComponent {
  constructor(public service: ProductDetailService, private toastr:ToastrService) { }


  onSubmit(form:NgForm){
    this.service.formSubmitted=true
    if(form.valid)
    {if(this.service.formData.id == ""){this.insertRecord(form)
      this.service.refreshList();
    }
    else this.updateRecord(form)
    this.service.refreshList();
    }
  }
  insertRecord(form: NgForm){
    this.service.postProductDetail()
  .subscribe({
    next: res => {
      this.service.list = res as ProductDetail[]
      this.service.resetForm(form)
      this.service.refreshList();
      this.toastr.success('Inserted successfully', 'Product Catalog')
    },
    error: err => {
    }
    })}
  updateRecord(form:NgForm){ this.service.putProductDetail()
    .subscribe({
      next: res => {
        this.service.list = res as ProductDetail[]
        this.service.resetForm(form)
        this.service.refreshList();
        this.toastr.info('Updated successfully', 'Product Catalog')
      },
      error: err => {console.log(err) 
      }
      })}
}
