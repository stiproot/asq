import { Component, OnInit, Output, Input, EventEmitter, ElementRef, ViewChild, OnDestroy, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-infinite-scroll',
  templateUrl: './infinite-scroll.component.html',
  styleUrls: ['./infinite-scroll.component.css']
})
export class InfiniteScrollComponent implements OnInit, OnDestroy, AfterViewInit{

  @Output() scrolled = new EventEmitter<HTMLElement>();
  @ViewChild('anchor') anchor: ElementRef<HTMLElement>;

  private observer: IntersectionObserver;

  constructor(private host: ElementRef) { }

  get element() {
    return this.host.nativeElement;
  }

  ngOnInit(): void { }

  ngAfterViewInit(): void{

    const options = {
      root: this.isHostScrollable() ? this.host.nativeElement : null
    };

    this.observer = new IntersectionObserver(([entry]) => {
      entry.isIntersecting && this.scrolled.emit();
    }, options);

    this.observer.observe(this.anchor.nativeElement);
  }

  ngOnDestroy(): void{
    this.observer.disconnect();
  }

  private isHostScrollable(){
    const style = window.getComputedStyle(this.element);
    return style.getPropertyValue('overflow') == 'auto' ||
      style.getPropertyValue('overflow-y') === 'scroll';
  }
}
