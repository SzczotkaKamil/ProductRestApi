import { Component, OnInit } from '@angular/core';
import { ProductDetailService } from '../shared/product-detail.service';
import { ProductDetail } from '../shared/product-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styles: [
  ]
})
export class ProductDetailsComponent implements OnInit {
constructor(public service: ProductDetailService, public toastr:ToastrService){

}

  ngOnInit(): void {
  this.service.refreshList();
  }
  populateForm(selectedRecord:ProductDetail){
     this.service.formData= Object.assign({},selectedRecord);
  }

  onDelete(id:string){
    if(confirm('Are you sure to delete this record?'))
this.service.deleteProductDetail(id)
.subscribe({
  next: res => {
    this.service.list = res as ProductDetail[]
    this.toastr.error('Deleted successfully', 'Product Catalog')
  },
  error: err => {console.log(err) 
  }
    
})

}
}
