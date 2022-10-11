const dates = {
  formattDateToDDMMYYYY(dateO) {
    const date = new Date(dateO);
    console.log(date);
    return 2;
  },
  getTimeDiffInDays(date1O, date2O) {
    let date1;
    let date2;

    if (!date2O) date2 = new Date();
    date1 = new Date(date1O);

    const diff = date2.getTime() - date1.getTime();
    return Math.round(diff / 1000 / 60 / 60 / 24);
  },
  ISOstringToDDMMYYYY(dateO) {
    const date = new Date(dateO);
    const dateString = `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`;
    console.log(dateString);
    return dateString;
  },
};

export default dates;
