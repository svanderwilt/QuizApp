import { Question } from './question';

export interface TestQuestion {
  testId: number;
  questionId: number;
  userAnswer: string;
  isCorrect: boolean;
  isAnswered: boolean;
  question: Question;
  testDone: boolean;
}
