import { Component, Input, OnInit } from '@angular/core';
import { TestQuestion } from '../_models/testQuestion';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-question-view',
  templateUrl: './question-view.component.html',
  styleUrls: ['./question-view.component.css'],
})
export class QuestionViewComponent implements OnInit {
  @Input() question: TestQuestion | undefined;
  data: {
    name?: string;
    isSelected?: boolean;
    isAnswer?: boolean;
    isUserAnswer?: boolean;
  }[] = [];

  constructor(private membersService: MembersService) {}

  ngOnInit(): void {
    this.randomizeOptions();
    this.loadData();
  }

  randomizeOptions() {
    this.question?.question.choices.sort(() => Math.random() - 0.5);
  }

  loadData() {
    for (var i = 0; i < 3; i++) {
      this.data.push({
        name: this.question?.question.choices[i],
        isSelected:
          this.question?.question.choices[i] ==
            this.question?.question.answer && this.question?.testDone,
        isAnswer:
          this.question?.question.choices[i] == this.question?.question.answer,
        isUserAnswer:
          this.question?.question.choices[i] == this.question?.userAnswer,
      });
      console.log(this.data[i]);
    }
  }

  answerQuestion() {}
}
