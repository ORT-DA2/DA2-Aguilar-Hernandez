import { Component } from '@angular/core';
import { Import } from '../../_type/import';
import { ArticleService } from '../../_services/article.service';

@Component({
  selector: 'app-import-article-page',
  templateUrl: './import-article-page.component.html',
  styleUrls: ['./import-article-page.component.css'],
})
export class ImportArticlePageComponent {
  value: string = '';
  importerType: string = '';
  importers: Import[] = [];
  token: string | null = '';

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
    this.loadImporters();
  }

  loadImporters(): void {
    this.articleService.getImporters().subscribe((importers: any) => {
      this.importers = importers;
    });
  }

  sendImport() {
    const importer: Import = {
      importerName: this.importerType,
      parameters: [
        {
          parameterType: '0',
          name: 'File Name',
          value: this.value,
        },
      ],
    };
    this.articleService
      .sendImporters(importer, this.token)
      .subscribe((importers: any) => {
        this.importers = importers;
      });
  }
}
