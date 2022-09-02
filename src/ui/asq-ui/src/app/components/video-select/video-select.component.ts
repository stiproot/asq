import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { environment } from '@environments/environment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-video-select',
  templateUrl: './video-select.component.html',
  styleUrls: ['./video-select.component.css']
})
export class VideoSelectComponent implements OnInit {

  @Input() requiredError: string;
  @Output() fileChanged = new EventEmitter<FormData>();

  formGroup: FormGroup;
  file: File;
  public fileName: string = '';

  private get _baseUrl(): string{
    return environment.baseUrl;
  }

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.iniForm();
  }

  iniForm(): void{
    this.formGroup = this.formBuilder.group({
      videoCtrl: ['', [Validators.required]]
    });
  }

  onFileChanged(event: any): void{

    this.file = event.target.files[0];
    this.fileName = this.file.name;

    const formData = new FormData();
    formData.append(this.file.name, this.file);

    this.fileChanged.emit(formData);
  }
}
