import { PercentPipe } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { Test } from '../_models/test';
import { TestQuestion } from '../_models/testQuestion';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;

  testCache = new Map();
  user: User | undefined;
  tests: Test[] = [];

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) {
          this.user = user;
          console.log(user.knownAs);
        }
      },
    });
  }

  getTests(pageNumber: number, pageSize: number) {
    let params = getPaginationHeaders(pageNumber, pageSize);

    return getPaginatedResult<Test[]>(
      this.baseUrl + 'users/' + this.user?.username + '/all-tests',
      params,
      this.http
    ).pipe(
      map((res) => {
        this.testCache.set(Object.values(params).join('-'), res);
        return res;
      })
    );
  }

  newTest(noOfQuestions: number) {
    return this.http.get<Test>(this.baseUrl + 'test');
  }

  getTest(testId: number) {
    return this.http.get<Test>(
      this.baseUrl + 'users/' + this.user?.username + '/' + testId
    );
  }

  deleteTest(testId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-test/' + testId);
  }

  answerQuestion(testId: number, questionId: number, answer: string) {
    return this.http.post<TestQuestion>(
      this.baseUrl + 'test/' + testId + '/' + questionId,
      { answer: answer }
    );
  }
}
