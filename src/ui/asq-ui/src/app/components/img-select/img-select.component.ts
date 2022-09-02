import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-img-select',
  templateUrl: './img-select.component.html',
  styleUrls: ['./img-select.component.css']
})
export class ImgSelectComponent implements OnInit {

  @Input() requiredError: string;
  @Input() public imgPreviewUrl: any;
  @Output() fileChanged = new EventEmitter<FormData>();

  readonly fileReader: FileReader = new FileReader();
  imgGroup: FormGroup;
  file: File;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.iniForm();
  }

  iniForm(): void{
    this.imgGroup = this.formBuilder.group({
      imgCtrl: ['', [Validators.required]]
    });
  }

  onFileChanged(event: any): void{

    this.file = event.target.files[0];

    this.fileReader.readAsDataURL(this.file);
    this.fileReader.onload = (e) => {
      this.imgPreviewUrl = this.fileReader.result.toString();
    };

    const formData = new FormData();
    formData.append(this.file.name, this.file);

    this.fileChanged.emit(formData);
  }
}
