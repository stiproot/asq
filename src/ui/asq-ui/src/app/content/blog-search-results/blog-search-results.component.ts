import { Component, OnInit, Input } from '@angular/core';
import { BlogPostSummaryDto } from '@app/_models/domain/blog-post-summary-dto';

@Component({
    selector: 'app-blog-search-results',
    templateUrl: './blog-search-results.component.html',
    styleUrls: [
        './blog-search-results.component.css',
        './../../shared/shared.search-results.style.css'
    ]
})
export class BlogSearchResultsComponent implements OnInit {

    @Input() blogPosts: BlogPostSummaryDto[] = null;

    constructor() { }

    ngOnInit(): void { }
}
