import { BlogPostFilterConfigDto } from './blog-post-filter-config-dto';
import { BlogPostSummaryDto } from './blog-post-summary-dto';

export class BlogPostQueryDto{
    public display: string = '';
    public config: BlogPostFilterConfigDto = null;
    public url: string = '';
    public data: BlogPostSummaryDto[] = [];
    public id: string = '';

    constructor(){ }
}
