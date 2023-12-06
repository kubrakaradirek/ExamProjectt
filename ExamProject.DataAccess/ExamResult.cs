﻿namespace ExamProject.DataAccess
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int? ExamId { get; set; }
        public Exam Exam { get; set; }
        public int QnAsId { get; set; }
        public QnAs QnAs { get; set; }
        public int Answer { get; set; }
    }
}