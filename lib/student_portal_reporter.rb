require 'capybara'
require 'capybara/poltergeist'

Capybara.register_driver :poltergeist_debug do |app|
  Capybara::Poltergeist::Driver.new(app, :inspector => true, :js_errors => false)
end

class StudentPortalReporter
  include Capybara::DSL

  LOGIN_URL = "https://student.portal.chalmers.se/_layouts/Chalmers/Authenticate.aspx?Source=/sv/studentliv/anmalanavarrangemang/Sidor/AnmalanAvArrangemang.aspx"
  ANMALAN_AV_ARRANGEMANG_URL = "https://student.portal.chalmers.se/sv/studentliv/anmalanavarrangemang/Sidor/AnmalanAvArrangemang.aspx?authenticated"
  SUCCESS_CONTENT = /Tack för din anmälan av detta arrangemang/i

  def initialize
    Capybara.default_driver = :poltergeist_debug
  end

  def correct_minute_string(minute_string)
    minute = (minute_string[3..4].to_i / 5) * 5
    minute.to_s.rjust(2, '0')
  end

  def party_report(reports)
    visit LOGIN_URL
    Rails.logger.info("Starting login")
    while current_url != ANMALAN_AV_ARRANGEMANG_URL
      Rails.logger.info("Trying to login.. ")
      begin
        within("#aspnetForm") do
          fill_in 'ctl00_ContentPlaceHolder1_UsernameTextBox', with: Rails.application.secrets.vo_usr
          fill_in 'ctl00_ContentPlaceHolder1_PasswordTextBox', with: Rails.application.secrets.vo_pwd
        end
      rescue Capybara::ElementNotFound
        save_and_open_page
        raise "Failed to login, saving page to app root."
      end
      #find_button('ctl00_ContentPlaceHolder1_SubmitButton').trigger('click')
      click_button('ctl00_ContentPlaceHolder1_SubmitButton')
    end
    Rails.logger.info("Succesfully logged in") 

    unless current_url == ANMALAN_AV_ARRANGEMANG_URL
      visit ANMALAN_AV_ARRANGEMANG_URL
    end

    sleep 0.5

    reports.each do |b|
      Rails.logger.info("Starting report")
      begin
        approval_type = b.liquor_license == '1' ? 'Sökt' : 'Ej aktuellt'
        deltagare = 75
        start_date = b.begin_date.strftime '%F'
        start_time = b.begin_date.strftime '%R'
        start_minute = correct_minute_string start_time
        end_date = b.end_date.strftime '%F'
        end_time = b.end_date.strftime '%R'
        end_minute = correct_minute_string end_time

          within("#aspnetForm") do
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl00_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.title
            select 'Hubben', from: 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl01_ctl00_ctl00_ctl04_ctl00_Lookup'
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl02_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: deltagare
            check 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl03_ctl00_ctl00_ctl04_ctl00_ctl00'
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl04_ctl00_ctl00_ctl04_ctl00_ctl00_DateTimeField_DateTimeFieldDate', with: start_date
            select start_time[0..2], from: 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl04_ctl00_ctl00_ctl04_ctl00_ctl00_DateTimeField_DateTimeFieldDateHours'
            select start_minute, from: 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl04_ctl00_ctl00_ctl04_ctl00_ctl00_DateTimeField_DateTimeFieldDateMinutes'
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl05_ctl00_ctl00_ctl04_ctl00_ctl00_DateTimeField_DateTimeFieldDate', with: end_date
            select end_time[0..2], from: 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl05_ctl00_ctl00_ctl04_ctl00_ctl00_DateTimeField_DateTimeFieldDateHours'
            select end_minute, from: 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl05_ctl00_ctl00_ctl04_ctl00_ctl00_DateTimeField_DateTimeFieldDateMinutes'
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl06_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.group
            select approval_type, from: 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl07_ctl00_ctl00_ctl04_ctl00_DropDownChoice'
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl08_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.party_responsible_name
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl09_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.party_responsible_phone
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl10_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.party_responsible_mail
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl11_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.co_party_responsible_name
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl12_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.co_party_responsible_phone
            fill_in 'ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl13_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: b.co_party_responsible_mail
            # fill_in 'ctl00_ctl19_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_ctl05_ctl14_ctl00_ctl00_ctl04_ctl00_ctl00_TextField', with: comments
          end
          # puts "Sent #{b.title} to Chalmers"
          find_button('ctl00_m_g_2ec8a987_c320_462d_8231_f85b57c1503e_ctl00_ctl00_toolBarTbl_RightRptControls_ctl00_ctl00_diidIOSaveItem').trigger('click')
          # page.driver.debug


          tries_left = 10
          until page.has_content?(SUCCESS_CONTENT) or tries_left == 0
            Rails.logger.info("Couldn't find success content yet. Tries left: #{tries_left}")
            tries_left-=1
            sleep 0.5
          end

          if page.has_content?(SUCCESS_CONTENT)
            Rails.logger.info("Report submitted")
          else
            save_and_open_page
            raise "error, Unable to see success content, check your mail to see if it was successful, saved page to app root"
          end

        visit ANMALAN_AV_ARRANGEMANG_URL

      rescue Capybara::ElementNotFound => e
        save_and_open_page
        raise "error, saved page to app root, #{e.message}"
      end
    end
  end
end
