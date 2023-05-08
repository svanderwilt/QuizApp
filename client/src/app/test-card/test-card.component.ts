import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { Test } from '../_models/test';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-test-card',
  templateUrl: './test-card.component.html',
  styleUrls: ['./test-card.component.css'],
})
export class TestCardComponent implements OnInit {
  @Input() test: Test | undefined;

  constructor(
    private memberService: MembersService,
    private toastr: ToastrService,
    private router: Router
  ) {}
  ngOnInit(): void {
    console.log(this.test?.testStartTime);
  }

  viewTest() {
    this.router.navigate(['/member/test/', this.test?.id]);
  }

  deleteTest() {
    if (this.test) {
      this.memberService.deleteTest(this.test.id).subscribe({
        next: () => {
          console.log('Test deleted');
        },
      });
    } else console.log('nothing to delete sir!');
  }
}
