# Answers – Delegates & Events

**Part 2 – What problem does `Func<>` solve?**
من غير Func هنضطر نعمل delegate جديد لكل signature (`delegate decimal PriceCalculator(Order o)`، `delegate string X(Book b)`...).
`Func<>` و`Action<>` و`Predicate<>` أنواع جاهزة generic بتغطي أغلب الـ signatures، فبنوفر الكود المتكرر.

**Part 3 – Why `Predicate<Order>` أوضح من `Func<Order,bool>`؟**
الاتنين شكلهم واحد، بس الاسم `Predicate` بيقول صراحة "ده شرط/اختبار على الـ Order" (نية الكود واضحة)،
وكتير من الـ APIs (زي `List<T>.Find` و`RemoveAll`) متعرفة بـ Predicate. والنوعين مش قابلين للتبديل مع بعض مباشرة.

**Q1** – `PriceCalculator` delegate type بنعرفه إحنا، و`Func<Order,decimal>` نوع جاهز generic بنفس الـ signature.
السلوك واحد، بس هما نوعين مختلفين (مفيش تحويل مباشر بينهم)، واستخدام Func بيوفر تعريف الـ delegate.

**Q2** – `Action<Order>` بيرجّع `void` (بينفذ حاجة) و`Func<Order,decimal>` بيرجّع قيمة (decimal). آخر type parameter في Func هو نوع الراجع.

**Q3** – `Predicate<T>` بيرجّع `bool` لأنه معمول للتعبير عن **شرط/اختبار**: هل العنصر ده بيحقق المعيار؟ (فلترة، تحقق، بحث).

**Q4** – الـ delegate نوع بيشاور على ميثود/ميثودز. الـ event هو delegate field بـ قيود: من بره الكلاس تقدر بس تعمل `+=` و`-=`،
وماتقدرش تنده عليه ولا تعمل له `=`. الـ event بيتبنى على delegate.

**Q5** – عشان الـ publisher (الكلاس صاحب الـ event) هو الوحيد اللي يقرر امتى الحدث يحصل. لو أي كود يقدر ينده عليه
هيبقى ممكن حد يعمل "fake" للحدث، وده بيكسر الـ Encapsulation.

**Q6** – كلهم بيتنفذوا بالترتيب اللي اشتركوا بيه (multicast). لو واحد رمى exception الباقيين مش بيتنفذوا (إلا لو اتعامل معاه).

**Q7** – `orderService.OrderProcessed += HandleOrderProcessed;`
- `orderService.OrderProcessed` : الـ event بتاع الـ service.
- `+=` : "اشترك / ضيف" الـ handler لقايمة المشتركين (مش بيمسح القديم).
- `HandleOrderProcessed` : الميثود اللي هتتنده لما الحدث يحصل (لازم signature تطابق `Action<Order>`).

**Q8** – لو عرّفنا `public Action<Order> OrderProcessed;` (delegate field عادي) فأي كود بره يقدر:
ينده عليه بنفسه، أو يكتب `= ...` ويمسح كل المشتركين. مع `event` بره الكلاس مسموح بس بالاشتراك وإلغاءه،
والـ Invoke حكر على الكلاس صاحب الـ event.

**Bonus** – `OrderService` بياخد `Func<Order, decimal> pricingStrategy` في الـ constructor ومبيعرفش بيتحسب إزاي؛
اللي بيستدعيه هو اللي يختار (Normal / 10% / 20% / VIP) من غير تعديل الـ service.
