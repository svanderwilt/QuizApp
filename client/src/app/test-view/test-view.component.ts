import {
  AfterViewInit,
  Component,
  Input,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscribable, take } from 'rxjs';
import { Test } from '../_models/test';
import { TestQuestion } from '../_models/testQuestion';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-test-view',
  templateUrl: './test-view.component.html',
  styleUrls: ['./test-view.component.css'],
})
export class TestViewComponent implements OnInit, OnDestroy {
  test: Test | undefined;
  user?: User;
  currQuestion: TestQuestion | undefined;
  testComplete: boolean = false;
  currPos: number = -1;
  skippedArray: number[] = [];
  selectedOption: number = 0;
  form: FormGroup;
  loading = false;

  constructor(
    private memberService: MembersService,
    private accountService: AccountService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder
  ) {
    this.form = this.formBuilder.group({
      options: ['', Validators.required],
    });
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
  }
  ngOnInit(): void {
    this.getTest();
  }

  getTest(): void {
    const id = Number(this.route.snapshot.paramMap.get('testId'));
    this.memberService.getTest(id).subscribe({
      next: (test) => {
        this.test = test;
        this.getNextQuestion();
      },
    });
  }

  getNextQuestion(): void {
    if (this.test) {
      this.currPos++;
      if (!this.test.completed) {
        while (
          this.currPos < this.test.testQuestions.length &&
          this.test.testQuestions[this.currPos].isAnswered
        ) {
          this.currPos++;
        }
        if (this.currPos >= this.test?.testQuestions.length) {
          this.testComplete = true;
        } else {
          this.currQuestion = this.test.testQuestions[this.currPos];
          this.randomizeOptions();
          this.form.reset();
          this.form.enable();
        }
      } else {
        if (this.currPos >= this.test.testQuestions.length) {
          this.currPos = 0;
        }
        this.currQuestion = this.test.testQuestions[this.currPos];
      }
    } else {
      console.log('no test!');
    }
    console.dir(this.test);
  }

  randomizeOptions() {
    this.currQuestion?.question.choices.sort(() => Math.random() - 0.5);
  }

  submit() {
    if (this.test && this.currQuestion) {
      if (this.currQuestion.isAnswered) {
        this.getNextQuestion();
      } else {
        this.loading = true;
        this.memberService
          .answerQuestion(
            this.test.id,
            this.currQuestion.question.id,
            this.currQuestion.question.choices[this.form.value.options]
          )
          .subscribe({
            next: (tq) => {
              this.test!.testQuestions[this.currPos] = tq;
              this.currQuestion!.isCorrect = tq.isCorrect;
              this.currQuestion!.isAnswered = tq.isAnswered;
              this.currQuestion!.userAnswer = tq.userAnswer;
              this.form.disable();
            },
          });
      }
    }
  }

  ngOnDestroy(): void {}
}
