import { Injectable } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';

@Injectable({
  providedIn: 'root'
})
export class OwlConfig{
    public OWL_OPTIONS: OwlOptions = {
        loop: true,
        mouseDrag: true,
        touchDrag: true,
        pullDrag: true,
        dots: false,
        navSpeed: 700,
        navText: ['', ''],
        responsive: {
            0: {
                items: 1
            },
            375: {
                items: 1
            },
            740: {
                items: 2
            },
            940: {
                items: 4
            }
        },
        nav: false
    }
}