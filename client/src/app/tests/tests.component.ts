import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pagination } from '../_models/pagination';
import { Test } from '../_models/test';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css'],
})
export class TestsComponent implements OnInit {
  pageNumber = 1;
  pageSize = 3;
  tests: Test[] | undefined;
  pagination: Pagination | undefined;

  constructor(private memberService: MembersService, private router: Router) {}
  ngOnInit(): void {
    this.loadTests();
  }

  loadTests() {
    this.memberService.getTests(this.pageNumber, this.pageSize).subscribe({
      next: (response) => {
        this.pagination = response.pagination;
        this.tests = response.result;
      },
    });
  }

  newTest() {
    this.memberService.newTest(20).subscribe({
      next: (t) => {
        this.tests?.unshift(t);
        if (this.tests)
          this.router.navigate(['/member/test/', this.tests[0].id]);
      },
    });
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadTests();
    }
  }
}
