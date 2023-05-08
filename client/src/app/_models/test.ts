import { TestQuestion } from './testQuestion';

export interface Test {
  id: number;
  userId: number;
  testStartTime: Date;
  testEndTime: Date;
  score: number;
  answeredNo: number;
  completed: boolean;
  testQuestions: TestQuestion[];
}
