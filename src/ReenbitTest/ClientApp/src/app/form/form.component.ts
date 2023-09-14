import { Component } from '@angular/core';
import { FormService } from './form.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent {
  selectedFile: File = null!;

  constructor(private appService: FormService, private toastrService: ToastrService) {}

  onFileChange(event: any) {
    this.selectedFile = event.target.files[0];
  }

  submitForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    file: new FormControl('', [Validators.required, Validators.pattern(/\.docx$/)])
  })

  onSubmit() {
    const formData = new FormData();
    formData.append('email', this.submitForm.get('email')!.value!);
    formData.append('file', this.selectedFile, this.selectedFile.name);

    this.appService.upload(formData).subscribe( {
      next: () => this.toastrService.success("Your file is successfully uploaded!"),
      error: (response) => {
        console.log(response);
        this.toastrService.error("Something went wrong!");
      }
    });
  }
}
